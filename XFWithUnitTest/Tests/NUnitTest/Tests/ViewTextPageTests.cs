﻿using System.Linq;
using NUnit.Framework;
using Prism.Ioc;
using Shouldly;
using XFWithUnitTest.Models;
using XFWithUnitTest.ViewModels;

namespace NUnitTest.Tests
{
    [TestFixture]
    public class ViewTextPageTests : BaseTest
    {
        /// <summary>
        /// Navigating to the View Text Page from the Home
        /// Page by selecting a Text item from the Text List
        /// </summary>
        [Test]
        public void NavigatingToViewTextPageTest()
        {
            // Is the app running
            App.ShouldNotBeNull();

            // Am I in the Home page
            GetCurrentPage().BindingContext.GetType().Name.ShouldBe(nameof(HomePageViewModel));

            // Navigating to New Text page
            App.Container.Resolve<HomePageViewModel>().NewTextCommand.Execute();

            // Create a new Text item
            App.Container.Resolve<NewTextPageViewModel>().TextItem = new TextItem()
            {
                TextTitle = "Juis yuwe sjkl Tywe oiq aklsjd asqw al.",
                Text = "Binf yuw tyasas pwerq asyu tui nuiwe aske yrwn kashdihas asju ywte.",
            };
            var newTextItem = App.Container.Resolve<NewTextPageViewModel>().TextItem;

            // I click on "SaveText" Button
            App.Container.Resolve<NewTextPageViewModel>().SaveTextCommand.Execute();

            // Am I in the Home page
            GetCurrentPage().BindingContext.GetType().Name.ShouldBe(nameof(HomePageViewModel));

            // Check if we see new Text Item in the List
            App.Container.Resolve<HomePageViewModel>().TextList.Count.ShouldBe(1);

            // Select a Text item from the Text List
            App.Container.Resolve<HomePageViewModel>().SelectedTextItem =
                App.Container.Resolve<HomePageViewModel>().TextList.First();

            // Am I in the View Text page
            GetCurrentPage().BindingContext.GetType().Name.ShouldBe(nameof(ViewTextPageViewModel));

            // Check the viewing Text item details
            var viewingTextItem = App.Container.Resolve<ViewTextPageViewModel>().TextItem;
            viewingTextItem.TextTitle.ShouldBe(newTextItem.TextTitle);
            viewingTextItem.Text.ShouldBe(newTextItem.Text);
            viewingTextItem.TextDateTime.ShouldBe(newTextItem.TextDateTime);
        }
    }
}