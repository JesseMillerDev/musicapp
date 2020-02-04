using System;
using Xunit;
using Egil.RazorComponents.Testing;
using Egil.RazorComponents.Testing.Diffing;
using Egil.RazorComponents.Testing.Asserting;
using Egil.RazorComponents.Testing.Mocking.JSInterop;
using Egil.RazorComponents.Testing.EventDispatchExtensions;
using MusicApp.Components;
using System.Collections.Generic;
using MusicApp.Models;

namespace MusicApp.Test
{
    public class SearchComponentTest : ComponentTestFixture
    {
        [Fact]
        public void SearchComponentRendersCorrectly()
        {
            var albums = new Album[] { new Album { Id = 1, Artist = "Blink 182", Category = "Happy Punk", Selected = false, Title = "Take Off Your Pants and Jacket" } };

            var cut = RenderComponent<SearchComponent>(CascadingValue(albums));

            cut.MarkupMatches(@"<div class=""columns"">
                            <div class=""column"">
                                <div class=""columns"">
                                    <div class=""column"">
                                        <input type = ""text"" class=""input is-medium"" placeholder=""Enter search query..."">
                                    </div>
                                    <div class=""column is-narrow"">
                                        <button id = ""Search"" class=""button is-primary is-medium"" type=""button"">Search</button>&nbsp;
                                        <button id = ""Clear"" class=""button is-warning is-medium"" type=""button"">Clear</button>
                                    </div>
                                </div>
                            </div>
                        </div>");
            
            cut.Instance.SearchQuery = "blink";
            cut.Find("#Search").Click();

            

            var clearButton = cut.Find("#Clear");
        }

        [Fact]
        public void SearchComponentSearchButtonClickedCallbackTriggered()
        {
            var albums = new Album[] { 
                new Album { Id = 1, Artist = "Blink 182", Category = "Happy Punk", Selected = false, Title = "Take Off Your Pants and Jacket" },
                new Album { Id = 2, Artist = "Twenty One Pilots", Category = "Schizo Pop", Selected = true, Title = "Trench" },
                new Album { Id = 3, Artist = "Blink 182", Category = "Happy Punk", Selected = false, Title = "Blink 182" }
            };

            var cut = RenderComponent<SearchComponent>(CascadingValue(albums));

            cut.Instance.SearchQuery = "blink";
            cut.Find("#Search").Click();

            Assert.Equal(2, cut.Instance.albumList.Length);
        }
    }
}