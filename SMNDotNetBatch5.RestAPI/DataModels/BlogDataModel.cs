namespace SMNDotNetBatch5.RestAPI.DataModels
{
    public class BlogDataModel
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
        public bool DeleteFlag { get; set; }

    }
}
