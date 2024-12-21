namespace ExamineActionsAPI
{
    public interface IExamineActionHasDependendency
	{
        public (string Author, string Name)[]? MelonDependency { get; set; }
        public string[]? GearNameDependency { get; set; }
        public string[]? CSharpTypeDependency { get; set; }
	}
}
