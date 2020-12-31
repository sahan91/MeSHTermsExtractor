using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MeSHTermExtractor.Models
{
    [XmlRoot("DescriptorRecordSet")]
    public class DescriptorRecordSet : List<DescriptorRecord> { }

    public class DescriptorRecord
    {
        public string DescriptorUI { get; set; }
        public DescriptorName DescriptorName { get; set; }
        public TreeNumberList TreeNumberList { get; set; }
        public DateCreated DateCreated { get; set; }
        public List<Concept> ConceptList { get; set; }
    }

    public class Concept
    {
        public string ConceptUI { get; set; }
        public List<Term> TermList { get; set; }
        public string ScopeNote { get; set; }
    }

    public class Term
    {
        public string String { get; set; }
    }

    public class DescriptorName
    {
        public string String { get; set; }
    }

    public class DateCreated
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }

        public override string ToString()
        {
            return $"{Day}-{Month}-{Year}";
        }
    }

    public class TreeNumberList : List<TreeNumber>
    {
    }

    public class TreeNumber
    {
    }
}