﻿using System.IO;
using System.Threading.Tasks;

namespace R4Mvc.Tools.Services
{
    public class GeneratedFileTesterService : IGeneratedFileTesterService
    {
        public async Task<bool> IsGenerated(Stream fileStream)
        {
            using (var fileReader = new StreamReader(fileStream))
            {
                bool foundComment = false, foundAttribute = false;
                string line;
                while ((line = await fileReader.ReadLineAsync()) != null)
                {
                    if (line.Contains("// <auto-generated />"))
                        foundComment = true;
                    if (line.Contains($"[GeneratedCode(\"{Constants.ProjectName}\""))
                        foundAttribute = true;

                    if (foundComment && foundAttribute)
                        return true;
                }
            }
            return false;
        }
    }
}