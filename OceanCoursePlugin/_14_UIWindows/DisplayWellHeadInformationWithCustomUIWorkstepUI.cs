using System;
using System.Drawing;
using System.Windows.Forms;

using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.DomainObject.Well;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.UI.Controls;

namespace OceanCoursePlugin._14_UIWindows
{
    /// <summary>
    /// This class is the user interface which forms the focus for the capabilities offered by the process.  
    /// This often includes UI to set up arguments and interactively run a batch part expressed as a workstep.
    /// </summary>
    partial class DisplayWellHeadInformationWithCustomUIWorkstepUI : UserControl
    {
        private DisplayWellHeadInformationWithCustomUIWorkstep workstep;
        /// <summary>
        /// The argument package instance being edited by the UI.
        /// </summary>
        private DisplayWellHeadInformationWithCustomUIWorkstep.Arguments args;
        /// <summary>
        /// Contains the actual underlaying context.
        /// </summary>
        private WorkflowContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayWellHeadInformationWithCustomUIWorkstepUI"/> class.
        /// </summary>
        /// <param name="workstep">the workstep instance</param>
        /// <param name="args">the arguments</param>
        /// <param name="context">the underlying context in which this UI is being used</param>
        public DisplayWellHeadInformationWithCustomUIWorkstepUI(DisplayWellHeadInformationWithCustomUIWorkstep workstep, DisplayWellHeadInformationWithCustomUIWorkstep.Arguments args, WorkflowContext context)
        {
            InitializeComponent();
            //
            this.workstep = workstep;
            this.args = args;
            this.context = context;
            //
            // register to argument package changes
            context.ArgumentPackageChanged += ContextOnArgumentPackageChanged;
            //
            // register for buttons events
            ApplyButton.Click += ApplyButtonOnClick;
            OkButton.Click += OkButtonOnClick;
            CancelButton.Click += CancelButtonOnClick;
            //
            // set the images for the buttons
            ApplyButton.Image = PetrelImages.Apply;
            OkButton.Image = PetrelImages.OK;
            CancelButton.Image = PetrelImages.Cancel;
            //
            // correct image aligment
            ApplyButton.ImageAlign = ContentAlignment.MiddleLeft;
            OkButton.ImageAlign = ContentAlignment.MiddleLeft;
            CancelButton.ImageAlign = ContentAlignment.MiddleLeft;
            //
            // register for drop behavoir
            WellDropper.DragDrop += WellDropperOnDragDrop;
        }

        private void CancelButtonOnClick(object sender, EventArgs eventArgs)
        {
            this.FindForm()?.Close();
        }

        private void OkButtonOnClick(object sender, EventArgs eventArgs)
        {
            ApplyButtonOnClick(sender, eventArgs);
            this.FindForm()?.Close();
        }

        private void ApplyButtonOnClick(object sender, EventArgs eventArgs)
        {
            // firstly update the arg pack
            UpdateArgPackFromUI();
            //
            // then notify other listeners that the arg pack has changed
            context.OnArgumentPackageChanged(this, new WorkflowContext.ArgumentPackageChangedEventArgs());
            //
            // if this Workstep is beings run as Proccess run the workstep
            if (context is WorkstepProcessWrapper.Context)
            {
                workstep.GetExecutor(args, null).ExecuteSimple();
                UpdateUIFromArgPack();
            }
        }

        private void ContextOnArgumentPackageChanged(object sender, WorkflowContext.ArgumentPackageChangedEventArgs argumentPackageChangedEventArgs)
        {
            if (Equals(sender, this)) return;
            //
            UpdateUIFromArgPack();
        }

        private void UpdateArgPackFromUI()
        {
            args.Well = WellPresenter.Tag as Borehole;
        }

        private void UpdateUIFromArgPack()
        {
            UpdateWellPresenterFromWell(WellPresenter, args.Well);
            // update unit labels
            WellHeadXUnit.Text = PetrelUnitSystem.GetDisplayUnit(Domain.X.UnitMeasurement).DisplaySymbol;
            WellHeadYUnit.Text = PetrelUnitSystem.GetDisplayUnit(Domain.Y.UnitMeasurement).DisplaySymbol;
            //
            // update well head text boxes
            WellHeadXTextBox.Value = args.WellHeadX;
            WellHeadYTextBox.Value = args.WellHeadY;
        }

        private static void UpdateWellPresenterFromWell(PresentationBox presenter, Borehole borehole)
        {
            var nameInfoFactory = CoreSystem.GetService<INameInfoFactory>(borehole);
            var imageInfoFactory = CoreSystem.GetService<IImageInfoFactory>(borehole);
            //
            var nameInfo = nameInfoFactory?.GetNameInfo(borehole);
            var imageInfo = imageInfoFactory?.GetImageInfo(borehole);
            //
            presenter.Text = nameInfo?.Name;
            presenter.Image = imageInfo?.GetDisplayImage(new ImageInfoContext());
            presenter.Tag = borehole;
        }

        private void WellDropperOnDragDrop(object sender, DragEventArgs e)
        {
            var borehole = e.Data.GetData(typeof(Borehole)) as Borehole;
            //
            // if the dropped item is not a bore hole return
            if (borehole == null) return;
            //
            // otherwise update the WellPresenter
            UpdateWellPresenterFromWell(WellPresenter, borehole);
        }
    }
}
