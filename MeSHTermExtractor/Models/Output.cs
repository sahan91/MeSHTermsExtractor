namespace MeSHTermExtractor.Models
{
    public class Output
    {
        public string DescriptorUI { get; set; }
        public string DescriptorName { get; set; }
        public string TermList { get; set; }
        public string ScopeNote { get; set; }
        public string TreeNumber { get; set; }
        public string DateCreated { get; set; }

        public override string ToString()
        {
            return $"{DescriptorUI}\t{DescriptorName}\t{TermList}\t{ScopeNote}\t{TreeNumber}\t{DateCreated}";
        }
    }
}