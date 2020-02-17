using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using OceanCoursePlugin.Extensions;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Contexts;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._14_UIWindows
{
    /// <summary>
    /// This class is the user interface which forms the focus for the capabilities offered by the process.  
    /// This often includes UI to set up arguments and interactively run a batch part expressed as a workstep.
    /// </summary>
    partial class TimeUnitConversionWithCustomUIWorkstepUI : UserControl
    {
        private TimeUnitConversionWithCustomUIWorkstep workstep;
        /// <summary>
        /// The argument package instance being edited by the UI.
        /// </summary>
        private TimeUnitConversionWithCustomUIWorkstep.Arguments args;
        /// <summary>
        /// Contains the actual underlaying context.
        /// </summary>
        private WorkflowContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeUnitConversionWithCustomUIWorkstepUI"/> class.
        /// </summary>
        /// <param name="workstep">the workstep instance</param>
        /// <param name="args">the arguments</param>
        /// <param name="context">the underlying context in which this UI is being used</param>
        public TimeUnitConversionWithCustomUIWorkstepUI(TimeUnitConversionWithCustomUIWorkstep workstep, TimeUnitConversionWithCustomUIWorkstep.Arguments args, WorkflowContext context)
        {
            InitializeComponent();

            this.workstep = workstep;
            this.args = args;
            this.context = context;
            //
            context.ArgumentPackageChanged += ContextOnArgumentPackageChanged;
            //
            ApplyButton.Click += ApplyButtonOnClick;
            OkButton.Click += OkButtonOnClick;
            CancelButton.Click += CancelButtonOnClick;
            //
            ApplyButton.Image = PetrelImages.Apply;
            OkButton.Image = PetrelImages.OK;
            CancelButton.Image = PetrelImages.Cancel;
        }

        private void OkButtonOnClick(object sender, EventArgs eventArgs)
        {
            ApplyButtonOnClick(sender, eventArgs);
            this.FindForm()?.Close();
        }

        private void CancelButtonOnClick(object sender, EventArgs eventArgs)
        {
            this.FindForm()?.Close();
        }

        private void ApplyButtonOnClick(object sender, EventArgs eventArgs)
        {
            UpdateArgPackFromUI();
            context.OnArgumentPackageChanged(this, new WorkflowContext.ArgumentPackageChangedEventArgs());
            //
            if (context is WorkstepProcessWrapper.Context)
            {
                workstep.GetExecutor(args, null).ExecuteSimple();
                UpdateUIFromArgPack();
            }
        }

        private void ContextOnArgumentPackageChanged(object sender, WorkflowContext.ArgumentPackageChangedEventArgs argumentPackageChangedEventArgs)
        {
            // Do nothing if the cause of change is this control
            if (Equals(sender, this)) return;
            //
            // Update UI
            UpdateUIFromArgPack();
        }

        private void UpdateUIFromArgPack()
        {
            PetrelLogger.InfoOutputWindow("enttring UpdateUIFromArgPack");
            //
            DisplayTimeValue.Text = args.DisplayTimeValue.ToString(CultureInfo.InvariantCulture);
            CoreTimeValue.Text = args.CoreTimeValue.ToString(CultureInfo.InvariantCulture);
            //
            DisplayTimeUnit.Text = args.DisplayTimeUnit;
            CoreTimeUnit.Text = args.CoreTimeUnit;
            //
            Refresh();
        }

        private void UpdateArgPackFromUI()
        {
            PetrelLogger.InfoOutputWindow("enttring UpdateArgPackFromUI");
            args.DisplayTimeValue = double.Parse(DisplayTimeValue.Text);
            //
            PetrelLogger.InfoOutputWindow(args.DumpToString());
        }
    }
}
