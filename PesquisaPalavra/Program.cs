using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace PesquisaPalavra
{
    class Program
    {
        public static int gatilho = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Digita a palavra que você deseja procurar abaixo!");

            string command = "";
            do
            {
                // Lê o comando selecionado
                command = Console.ReadLine();
                int posicao = PesquisaPalavra(command);

                if (posicao != -1)
                {
                    Console.WriteLine("Palavra " + command.ToString() + " encontrada na posição: " + posicao.ToString());
                    Console.WriteLine();
                    Console.WriteLine("Gatilhos gastos: " + gatilho.ToString());
                }
                else
                {
                    Console.WriteLine("Palavra " + command.ToString() + " não existente no dicionário!");
                }
                



            } while (command != "exit");

        }

        private static int PesquisaPalavra(string word)
        {
            //string[] letras = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string url = "http://teste.way2.com.br/dic/api/words/{0}";
            bool bNaoEncontrou = true;
            int linha = 0;
            WebClient cliente = new WebClient();
            cliente.Credentials = new NetworkCredential();
            int min = 0;
            int max = 1;


            while (bNaoEncontrou || min < max)
            {
                string w = word;
                string wl = cliente.DownloadString(string.Format(url, linha.ToString()));
                int tw = word.Length;
                int twl = wl.Length;

                if (!string.IsNullOrEmpty(wl))
                {
                    gatilho++;

                    if (tw == twl)
                    {
                        //Console.WriteLine(w);
                        for (int i = 0; i < tw; i++)
                        {
                            string charW = w.Substring(i, 1).ToUpper();
                            string charWl = wl.Substring(i, 1).ToUpper();
                            if (charW == charWl)
                            {
                                bNaoEncontrou = false;
                            }
                            else
                            {
                                bNaoEncontrou = true;
                                break;
                            }
                        }
                    }

                    linha++;
                }
                else
                {
                    linha = -1;
                    bNaoEncontrou = false;
                }
            }
            return linha;
        }
    }
}
