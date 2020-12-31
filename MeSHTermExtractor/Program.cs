using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using MeSHTermExtractor.Models;

namespace MeSHTermExtractor
{
    class Program
    {
        private static readonly string _inputFolder = "";
        private static readonly string _outputFolder = "";
        
        private const string FileName = "desc2021";
        private static readonly string[] Headings = {"DescriptorUI", "DescriptorName", "TermList", "ScopeNote", "TreeNumber", "DateCreated"};

        static void Main(string[] args)
        {
            var fullPath = $"{_inputFolder}{FileName}.xml";
            var doc = XDocument.Load(fullPath);
            
            var articleElements = doc.Descendants(nameof(DescriptorRecordSet));
            var outputs = new List<Output>();
            
            foreach (var article in articleElements.Elements(nameof(DescriptorRecord)))
            {
                var treeNums = article.Element(nameof(DescriptorRecord.TreeNumberList))
                    ?.Descendants(nameof(TreeNumber)).Select(x => $"{x.Value}");

                var concepts = article.Element(nameof(DescriptorRecord.ConceptList))
                    .Descendants(nameof(Concept));

                var scopeNote = concepts.Descendants(nameof(Concept.ScopeNote)).Select(x => string.IsNullOrEmpty(x.Value) ? "" : x.Value.Trim());

                var terms = concepts.Descendants(nameof(Concept.TermList)).Elements("Term").Elements("String").Select(x => x.Value);

                var fullDate = article.Element(nameof(DescriptorRecord.DateCreated));
                var day = fullDate.Element("Day").Value;
                var month = fullDate.Element("Month")?.Value;
                var year = fullDate.Element("Year")?.Value;

                var output = new Output()
                {
                    DescriptorUI = article.Element(nameof(DescriptorRecord.DescriptorUI))?.Value,
                    DateCreated = $"{day}-{month}-{year}",
                    DescriptorName = article.Element(nameof(DescriptorRecord.DescriptorName))?.Value,
                    TreeNumber = string.Join(";", treeNums ?? Array.Empty<string>()),
                    ScopeNote = string.Join("", scopeNote),
                    TermList = string.Join(";", terms)
                };
                outputs.Add(output);
            }
            Write(outputs, "test");
        }
        
        private static void Write(IEnumerable<Output> output, string fileName)
        {
            using var outputFile = new StreamWriter(Path.Combine(_outputFolder, $"{fileName}.tsv"));
            outputFile.WriteLine(string.Join("\t", Headings));
            foreach (var line in output)
                outputFile.WriteLine(line);
        }
    }
}