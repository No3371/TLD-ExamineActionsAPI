
using Il2Cpp;
using MelonLoader;
using UnityEngine;
using Il2CppTLD.Gear;

namespace ExamineActionsAPIDemo
{
    internal class ExamineActionsAPIDemo : MelonMod
    {
		internal static ExamineActionsAPIDemo Instance { get; private set; }
        public override void OnInitializeMelon()
		{
			Instance = this;
			ExamineActionsAPI.ExamineActionsAPI.Register(new TestActionPaperFromBooks());
			ExamineActionsAPI.ExamineActionsAPI.Register(new TestActionBruteForceSharpening());
			ExamineActionsAPI.ExamineActionsAPI.Register(new TestActionSliceMeat());
			ExamineActionsAPI.ExamineActionsAPI.Register(new TestActionAcornPreparing());
		}

	}
}