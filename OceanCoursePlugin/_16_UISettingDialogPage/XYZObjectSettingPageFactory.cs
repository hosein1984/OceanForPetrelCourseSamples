using System;
using OceanCoursePlugin._15_UITrees;
using Slb.Ocean.Petrel.UI;

namespace OceanCoursePlugin._16_UISettingDialogPage
{
    /// <summary>
    /// Dialog pages are displayed as a tab page in the settings windows of the tree items.
    /// The DialogageFactory adds the dialog page to the tab list every time it is displayed.
    /// </summary>
    class XYZObjectSettingPageFactory : IDialogPageFactory
    {
        public XYZObjectSettingPageFactory()
        {
        }

        #region IDialogPageFactory Members

        /// <summary>
        /// This method creates the pages which will extend the settings pages.
        /// Called every time the user displays the settings page of an item, right after the IsMatch
        /// method.
        /// </summary>
        /// <remarks>
        /// The domainObject parameter can be togglewindow, or anything, which has settings.
        /// </remarks>
        /// <param name="domainObject">The domain object which settings are to be displayed.</param>
        /// <param name="settingsPages">The list to add new settings page(s).</param>
        public void CreatePages(object domainObject, DialogPageCollection settingsPages)
        {
            // TODO: finish the implementation
            var xyzObject = domainObject as XYZObject;
            XYZObjectSettingPage xyzObjectUserControl = new XYZObjectSettingPage(xyzObject);
            //
            DialogPage page = new DialogPage("Properties", (dialogPage, o) => xyzObjectUserControl)
            {
                DomainObject = domainObject
            };
            //
            page.Apply += xyzObjectUserControl.ApplyCallback;
            //
            settingsPages.Add(page);
        }

        /// <summary>
        /// Gives back that the given domain object can be displayed by the settings page, or not.
        /// This method called every time the user displays the settings page of an item, right before 
        /// the CreatePages method.
        /// </summary>
        /// <remarks>
        /// The domainObject parameter can be togglewindow, or anything, which has settings.
        /// </remarks>
        /// <param name="domainObject">The domain object which's settings page will be displayed.</param>
        /// <returns>True, when the dialog page can display data, false, when not.</returns>
        public bool IsMatch(object domainObject)
        {
            return domainObject is XYZObject;
        }

        #endregion
    }
}