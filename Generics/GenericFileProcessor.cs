using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Generics
{
    public static class GenericFileProcessor
    {
        // where T : class => T precisa ser uma classe
        // where T : new() => T precisa ter um construtor vazio, pq T vai ser instanciado neste método
        public static List<T> LoadFromFile<T>(string path) where T : class, new()
        {
            if (!File.Exists(path)) throw new Exception($"Arquivo {path} não econtrado");

            var lines = File.ReadAllLines(path).ToList();
            var output = new List<T>();
            var entry = new T();

            //propriedades da classe passada
            var cols = entry.GetType().GetProperties();

            //o arquivo possui um cabeçalho com o nome das colunas
            if (lines.Count < 2) throw new Exception("Arquivo vazio");

            var headers = lines[0].Split(',');

            //remove o cabeçalho
            lines.RemoveAt(0);

            //percorre as linhas
            foreach (var row in lines)
            {
                entry = new T();
                var vals = row.Split(',');

                //percorre as posições do cabeçalho
                for (int i = 0; i < headers.Length; i++)
                {
                    //busca a propriedade baseado no nome da coluna/header
                    var col = cols.FirstOrDefault(c => c.Name == headers[i]);

                    if (col == null) throw new Exception($"Propridade {headers[i]} não encontrada");

                    //seta o valor da propriedade do objeto entry
                    col.SetValue(entry, Convert.ChangeType(vals[i], col.PropertyType));
                }

                //depois de setar todas as propriedades e adiciona na lista de saída
                output.Add(entry);
            }

            return output;
        }

        public static void SaveToTextFile<T>(List<T> data, string path) where T : class
        {
            var lines = new List<string>();
            var line = new StringBuilder();

            if (data == null || data.Count == 0)
            {
                throw new ArgumentNullException("data", "O parâmetro não pode ser nulo ou vazio");            
            }

            var cols = data[0].GetType().GetProperties();

            //monta o cabeçalho
            foreach (var col in cols)
            {
                line.Append(col.Name);
                line.Append(",");
            }

            //adiciona o cabeçalho
            lines.Add(line.ToString().Substring(0, line.Length - 1));

            //monta cada linha
            foreach (var row in data)
            {
                line = new StringBuilder();

                //percorre as propriedades da classe passada
                foreach (var col in cols)
                {
                    //adiciona cada campo do objeto
                    line.Append(col.GetValue(row));
                    line.Append(",");
                }

                //adiciona cada item da lista formatado para texto
                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }

            File.WriteAllLines(path, lines);
        }
    }
}
