namespace ExamineActionsAPI
{
    public interface IExamineActionHasDependendency
	{
        public string[]? MelonDependency { get; set; }
        public string[]? GearNameDependency { get; set; }
        public string[]? CSharpTypeDependency { get; set; }
	}
}
