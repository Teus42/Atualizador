using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using static System.Windows.Forms.LinkLabel;

namespace Atualizador
{
    class WebJson
    {
        //Json baseado no exemplo da API 
        static WebJson()
        {
            ServicePointManager.Expect100Continue = false; 
        }

        /* 
         * Metodo Send (do exemplo) que faz a conversação via Http com o equipamento, para isso necessario informar
         * uri (link sem o fcgi e sessão, porém com a requisição da API), data (quais dados a requisição precisa)
         * e o session, caso seja necessario a sessão
         */

        public static string Send(string uri, string data, string session = null)
        {
            /*
             * If basicamente só para não ter que informar o fcgi ou a sessão em cada requisição, 
             * ele também verifica se a sessão é nula se for ele não aplica na uri.
             * Simples mas muito útil
             */
             
            if (session != null)
            {
                uri += ".fcgi?session=" + session;
            }
            else
            {
                uri += ".fcgi";
            }

            /*
             * O try realiza a criação da sessão, já o catch a exceção caso ocorra algum erro
             */
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);

                //Informando de acordo com a API
                request.ContentType = "application/json"; 
                request.Method = "POST"; 

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var response = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    //Response não pode ser nula e para não quebrar a aplicação enquando roda criei essa exceção
                    if (response == null)
                    {
                        throw new Exception("Error response is null");
                    }

                    using (Stream responseData = response.GetResponseStream())
                    using (var reader = new StreamReader(responseData))
                    {
                        throw new Exception(reader.ReadToEnd());                                                
                    }
                }
            }
        }

        /* 
         * Método que criei para poder "clicar" nos botões do recovery,
         * ele é bem simples recebendo a uri do que será preciso baixar ou clicar
         * no caso ele também puxa o status do cgi durante a atualização e retornando essa informação.
         * No caso dos botões vai retornar um OK
         */

        public static string DownClient(string uri) 
        {
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();

            Encoding encode = Encoding.GetEncoding("utf-8");

            StreamReader result = new StreamReader(receiveStream, encode);

            string client = new WebClient().DownloadString(uri);

            return client; 
        }
    }
}