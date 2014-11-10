﻿using System;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Typewriter.Templates;
using Typewriter.VisualStudio.Resources;

namespace Typewriter.VisualStudio
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    //[ProvideEditorFactory(typeof(EditorFactory), 101)]
    //[ProvideEditorExtension(typeof(EditorFactory), ".tst", 32, NameResourceID = 101)]
    [Guid(GuidList.VisualStudioExtensionPackageId)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    //[ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class ExtensionPackage : Package
    {
        //private EditorFactory editorFactory;

        private DTE dte;
        private Log log;
        private ISolutionMonitor solutionMonitor;
        private ITemplateManager templateManager;
        private IEventQueue eventQueue;
        //private CommandManager commendManager;

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            //this.editorFactory = new EditorFactory(this);
            //base.RegisterEditorFactory(this.editorFactory);

            this.dte = GetService(typeof(DTE)) as DTE;

            if (this.dte == null)
                ErrorHandler.ThrowOnFailure(1);

            this.log = new Log(dte);
            this.solutionMonitor = new SolutionMonitor();
            this.templateManager = new TemplateManager(log, dte, solutionMonitor);
            this.eventQueue = new EventQueue(log);
            var generationManager = new GenerationManager(log, dte, solutionMonitor, templateManager, eventQueue);
            //this.commendManager = new CommandManager(generationManager);
        }
    }
}