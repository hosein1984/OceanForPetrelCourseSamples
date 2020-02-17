using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using OceanCoursePlugin._15_UITrees;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._16_UISettingDialogPage
{
    /// <summary>
    /// Dialog pages are displayed as a tab page in the settings windows of the tree items.
    /// </summary>
    partial class XYZObjectSettingPage : UserControl
    {
        private readonly XYZObject _xyzObject;

        public XYZObjectSettingPage(XYZObject xyzObject)
        {
            this.InitializeComponent();
            //
            _xyzObject = xyzObject;
            //
            XTextBox.Text = xyzObject.X.ToString(CultureInfo.InvariantCulture);
            YTextBox.Text = xyzObject.Y.ToString(CultureInfo.InvariantCulture);
            ZTextBox.Text = xyzObject.Z.ToString(CultureInfo.InvariantCulture);
            RadiusTextBox.Text = xyzObject.Radius.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// This method will be called by the Petrel system, when the user
        /// accepts the changes made on the settings pages.
        /// <seealso cref="OnApply"/>
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Cancellable argument, so the acceptance of the changes can be cancelled.</param>
        public void ApplyCallback(object sender, CancelEventArgs e)
        {
            e.Cancel = OnApply();
        }

        /// <summary>
        /// This method makes the changes on the settings data.
        /// </summary>
        /// <returns>True if everything went OK, otherwise false.</returns>
        public bool OnApply()
        {
            //
            float x;
            float y;
            float z;
            float radius;
            //
            if (float.TryParse(XTextBox.Text, out x) &&
                float.TryParse(YTextBox.Text, out y) &&
                float.TryParse(ZTextBox.Text, out z) &&
                float.TryParse(RadiusTextBox.Text, out radius))
            {
                if (radius <= 0)
                {
                    PetrelLogger.ErrorBox("Radius can not be negative");
                    return false;
                }
                //
                _xyzObject.X = x;
                _xyzObject.Y = y;
                _xyzObject.Z = z;
                _xyzObject.Radius = radius;
                //
                return true;
            }
            //
            // if we are here, it means that parsing failed
            PetrelLogger.ErrorBox("One of the provided values could be parsed successfuly");
            return false;

        }
    }
}
