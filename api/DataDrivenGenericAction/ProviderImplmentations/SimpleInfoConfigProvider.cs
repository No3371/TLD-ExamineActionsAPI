// #define VERY_VERBOSE
using Il2Cpp;

namespace ExamineActionsAPI.DataDrivenGenericAction
{
    public class SimpleInfoConfigProvider : IInfoConfigProvider
    {
        public struct InfoItem
        {
            [TinyJSON2.Include]
            public string Title { get; set; }
            [TinyJSON2.Include]
            public string Content { get; set; }
            [TinyJSON2.Include]
            public bool ContentLocalized { get; set; }
            [TinyJSON2.Include]
            public string? StandardTitleColor { get; set; } // BRIGHT_WHITE, SHALLOW_WHITE, SECONDARY_COLOR, WARNING_COLOR, DISABLED_COLOR
            public string? StandardContentColor { get; set; } // BRIGHT_WHITE, SHALLOW_WHITE, SECONDARY_COLOR, WARNING_COLOR, DISABLED_COLOR
            [TinyJSON2.Include]
            public UnityEngine.Color? CustomTitleColor { get; set; }
            [TinyJSON2.Include]
            public UnityEngine.Color? CustomContentColor { get; set; }
        }
        [TinyJSON2.Include]
        public List<InfoItem>? InfoItems { get; set; }
        public void GetInfoConfigs(ExamineActionState state, List<InfoItemConfig> configs)
        {
            foreach (var item in InfoItems)
            {
                var conf = new InfoItemConfig(
                    new LocalizedString() { m_LocalizationID = item.Title ?? "" },
                    item.Content
                );
                if (item.CustomTitleColor != null)
                {
                    conf.LabelColor = item.CustomTitleColor;
                }
                else switch (item.StandardTitleColor)
                {
                    case "BRIGHT_WHITE":
                        conf.LabelColor = ExamineActionsAPI.BRIGHT_WHITE;
                        break;
                    case "SHALLOW_WHITE":
                        conf.LabelColor = ExamineActionsAPI.SHALLOW_WHITE;
                        break;
                    case "SECONDARY_COLOR":
                        conf.LabelColor = ExamineActionsAPI.SECONDARY_COLOR;
                        break;
                    case "WARNING_COLOR":
                        conf.LabelColor = ExamineActionsAPI.WARNING_COLOR;
                        break;
                    case "DISABLED_COLOR":
                        conf.LabelColor = ExamineActionsAPI.DISABLED_COLOR;
                        break;
                    default:
                        conf.LabelColor = ExamineActionsAPI.SECONDARY_COLOR;
                        break;
                }

                if (item.CustomContentColor != null)
                {
                    conf.ContentColor = item.CustomContentColor;
                }
                else switch (item.StandardContentColor)
                {
                    case "BRIGHT_WHITE":
                        conf.ContentColor = ExamineActionsAPI.BRIGHT_WHITE;
                        break;
                    case "SHALLOW_WHITE":
                        conf.ContentColor = ExamineActionsAPI.SHALLOW_WHITE;
                        break;
                    case "SECONDARY_COLOR":
                        conf.ContentColor = ExamineActionsAPI.SECONDARY_COLOR;
                        break;
                    case "WARNING_COLOR":
                        conf.ContentColor = ExamineActionsAPI.WARNING_COLOR;
                        break;
                    case "DISABLED_COLOR":
                        conf.ContentColor = ExamineActionsAPI.DISABLED_COLOR;
                        break;
                    default:
                        conf.ContentColor = ExamineActionsAPI.BRIGHT_WHITE;
                        break;
                }
                configs.Add(conf);
            }
        }
    }
}
