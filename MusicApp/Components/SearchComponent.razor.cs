using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MusicApp.Models;
using MusicApp.Pages;
using System;
using System.Linq;

namespace MusicApp.Components
{
	public class SearchComponentBase : ComponentBase
	{
        [CascadingParameter] SearchResults Results { get; set; }
        protected string SearchQuery { get; set; }
        public string SearchCategory { get; set; }
        public Album[] albumList { get; set; }

        protected void SearchClick()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                return;
            }

            Search();
        }

        protected void Search()
        {
            albumList = Results.Albums;

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                SearchQuery = SearchQuery.ToLower();

                var newList = albumList.Where(a =>
                    a.Title.ToLower().Contains(SearchQuery) ||
                     a.Artist.ToLower().Contains(SearchQuery)).ToArray();

                Results.AlbumsChanged(newList);
            }
            else
            {
                Results.GetAlbums();
            }
        }

        protected void KeyUp(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" || e.Key == "Return")
            {
                Console.WriteLine("Enter pressed.");
                SearchClick();
            }
        }

        protected void ClearClick()
        {
            SearchQuery = "";
            SearchCategory = "";
            Search();
        }
    }
}
