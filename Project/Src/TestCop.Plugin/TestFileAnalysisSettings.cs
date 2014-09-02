﻿// --
// -- TestCop http://testcop.codeplex.com
// -- License http://testcop.codeplex.com/license
// -- Copyright 2013
// --

using System.Collections.Generic;
using System.Linq;
using JetBrains.Application;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Settings;

namespace TestCop.Plugin
{    
    [SettingsKey(typeof (CodeInspectionSettings), "Testing Attributes")]
    public class TestFileAnalysisSettings
    {     
        public IList<string> TestingAttributes 
        { 
            get
            {
                List<string> list = (TestingAttributeText ?? "").Split(',').ToList();
                list.RemoveAll(string.IsNullOrEmpty);
                return list;
            }
        }

        public IList<string> BddPrefixes
        {
            get
            {
                List<string> list = (BddPrefix ?? "").Split(',').ToList();
                list.RemoveAll(string.IsNullOrEmpty);
                return list;
            }
        }
        
        [SettingsEntry("TestClass,TestMethod,TestFixture,Test,Fact", "Testing Attributes to detect")]
        public string TestingAttributeText { get; set; }

        [SettingsEntry("Given,When", "Context prefixes for class names")]
        public string BddPrefix { get; set; }

        [SettingsEntry(false, "Do we look for any reference or just in files with similar names")]
        public bool FindAnyUsageInTestAssembly { get; set; }

        [SettingsEntry(true, "Check the namespace of the test matching the class under test")]
        public bool CheckTestNamespaces { get; set; }

        [SettingsEntry("Tests", "Suffix to always be applied to Test classes")]
        public string TestClassSuffix { get; set; }

        [SettingsEntry(@"Global::Ctrl+G, Ctrl+T", "Keyboard shortcut for switching between code and unit test files")]
        public string ShortcutToSwitchBetweenFiles { get; set; }

        [SettingsEntry(@"Class", "Name of Template to use when creating a code class")]
        public string CodeFileTemplateName { get; set; }

        [SettingsEntry(@"Class", "Name of Template to use when creating a unittest class")]
        public string UnitTestFileTemplateName { get; set; }   
        
        [SettingsEntry(@"true", "Should the TestCop output panel be opened on startup")]
        public bool OutputPanelOpenOnKeyboardMapping { get; set; }

        [SettingsEntry('.', "The char separator be used when naming test files to separate class from description e.g. ClassA_SecurityTests")]
        public char SeparatorUsedToBreakUpTestFileNames { get; set; }

        [SettingsEntry(@"^(.*?)\.?Tests$", "Regex to identify tests project by their namespace")]
        public string TestProjectToCodeProjectNameSpaceRegEx { get; set; }

        [SettingsEntry(@"", "RegEx replacement text")]
        public string TestProjectToCodeProjectNameSpaceRegExReplace { get; set; }


        [SettingsEntry(@"false", "Should the TestCop be configured for a single test project per solution")]
        public bool ConfiguredForSingleTestProject { get; set; }
        
        [SettingsEntry(@"^(.*?)\.?Tests(\..*?)(\..*)*$", "Regex for test namespace within single test assembly solutions")]
        public string SingleTestRegexTestToAssembly { get; set; }

        [SettingsEntry(@"$1$2", "Regex replace for test namespace within single test assembly solutions to identify namespace of code assembly")]
        public string SingleTestRegexTestToAssemblyProjectReplace { get; set; }

        [SettingsEntry(@"$3", "Regex replace for test namespace within single test assembly solutions to identify sub-namespace of code assembly")]
        public string SingleTestRegexTestToAssemblyProjectSubNamespaceReplace { get; set; }

        [SettingsEntry(@"^(.*?\..*?)(\..*?)$", "Regex for code namespace within single test assembly solutions")]
        public string SingleTestRegexCodeToTestAssembly { get; set; }

        [SettingsEntry(@"$2", "Regex replace for code namespace within single test assembly solutions to identify namespace of test assembly")]
        public string SingleTestRegexCodeToTestReplace { get; set; }

        [SettingsEntry(true, "Search project folders for files not part of the project")]
        public bool FindOrphanedProjectFiles { get; set; }

        [SettingsEntry("*.cs|*.aspx|*.jpg", "Pattern for orphaned files")]
        public string OrphanedFilesPatterns { get; set; }
    }

    [ShellComponent]
    public class TestCopSettingsManager
    {
        private readonly ISettingsStore _settingsStore;

        public TestCopSettingsManager(ISettingsStore settingsStore)
        {
            _settingsStore = settingsStore;
        }

        public static TestCopSettingsManager Instance
        {
            get
            {
                return Shell.Instance.GetComponent<TestCopSettingsManager>();
            }
        }

        public TestFileAnalysisSettings Settings
        {
            get
            {

                IContextBoundSettingsStore context = _settingsStore.BindToContextTransient(ContextRange.ApplicationWide);
                
                var testFileAnalysisSettings =
                    context.GetKey<TestFileAnalysisSettings>(SettingsOptimization.OptimizeDefault);
                return testFileAnalysisSettings;
            }
        } 
    }
}

