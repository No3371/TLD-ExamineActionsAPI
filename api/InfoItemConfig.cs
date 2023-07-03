using Il2Cpp;

namespace ExamineActionsAPI
{
    public class InfoItemConfig
	{
        public InfoItemConfig(LocalizedString title, string content)
        {
            Title = title;
            Content = content;
        }

        public LocalizedString Title { get; set; }
		public string Content { get; set; }
	}
}
