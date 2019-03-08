using System;
using System.Collections.Generic;
using Generics.Models;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            var usuarios = new List<Usuario>();
            var logs = new List<LogEntry>();

            var pathUsuarios = "usuarios.csv";
            var pathlogs = "logs.csv";

            popularListas(usuarios, logs);

            GenericFileProcessor.SaveToTextFile<Usuario>(usuarios, pathUsuarios);
            GenericFileProcessor.SaveToTextFile<LogEntry>(logs, pathlogs);

            var usuariosNoArquivo = GenericFileProcessor.LoadFromFile<Usuario>(pathUsuarios);

            Console.WriteLine("Usuários");
            foreach (var item in usuariosNoArquivo)
            {
                Console.WriteLine($"{item.UserName} ({item.NomeCompleto}) Ativo = {item.Ativo}");
            }

            var logsNoArquivo = GenericFileProcessor.LoadFromFile<LogEntry>(pathlogs);

            Console.WriteLine();
            Console.WriteLine("Logs");
            foreach (var item in logsNoArquivo)
            {
                Console.WriteLine($"{item.CodigoErro}: {item.Mensagem} às  {item.DataErro.ToString("dd/MM/yyyy HH:mm")}");
            }

            Console.ReadLine();

        }

        private static void popularListas(List<Usuario> usuarios, List<LogEntry> logs)
        {
            usuarios.Add(new Usuario() { UserName = "joacir", NomeCompleto = "Joacir Alves", Ativo = true });
            usuarios.Add(new Usuario() { UserName = "wilson", NomeCompleto = "Wilson Silva", Ativo = false });
            usuarios.Add(new Usuario() { UserName = "Caetano", NomeCompleto = "Caetano Santos", Ativo = true });

            logs.Add(new LogEntry() { CodigoErro = 404, Mensagem = "Página não encontrada", DataErro = DateTime.Now.AddDays(-4) });
            logs.Add(new LogEntry() { CodigoErro = 50023, Mensagem = "Erro de permissão", DataErro = DateTime.Now.AddHours(-12) });
            logs.Add(new LogEntry() { CodigoErro = 12, Mensagem = "Diretório não existente", DataErro = DateTime.Now.AddDays(-1) });
        }
    }
}
