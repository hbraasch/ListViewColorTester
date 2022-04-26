using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewColorTester
{
    internal class StartupPage : ContentPage
    {
        public class DisplayItem
        {
            public string Name { get; set; }

        }
        public class GroupedDisplayItem : ObservableCollection<DisplayItem>
        {
            public string Name { get; set; }
        }

        public ObservableCollection<GroupedDisplayItem> GroupDisplayItems { get; set; }
        public StartupPage()
        {
            GroupDisplayItems = new ObservableCollection<GroupedDisplayItem>();
            for (int i = 0; i < 3; i++)
            {
                var groupedDisplayItem = new GroupedDisplayItem { Name = $"Group {i}"};
                for (int j = 0; j < 3; j++)
                {
                    var displayItem = new DisplayItem { Name = $"Item {j}"};
                    groupedDisplayItem.Add(displayItem);
                }
                GroupDisplayItems.Add(groupedDisplayItem);
            }

            var listView = new ListView()
            {
                ItemsSource = GroupDisplayItems,
                SelectionMode = ListViewSelectionMode.Single,
                HasUnevenRows = false,
                IsGroupingEnabled = true,
                BackgroundColor = Colors.Transparent,
                SeparatorColor = Colors.Orange,
                Margin = 5
            };

            listView.ItemTemplate = new DataTemplate(typeof(ListViewDataTemplate));
            listView.GroupHeaderTemplate = new DataTemplate(typeof(ListViewGroupTemplate));

            Title = "StartupPage";
            BackgroundColor = Colors.Black;
            Content = listView;
        }

        public class ListViewDataTemplate : ViewCell
        {

            public ListViewDataTemplate()
            {

                var label = new Label { VerticalTextAlignment = TextAlignment.Center, TextColor = Colors.White, FontAttributes = FontAttributes.Italic, BackgroundColor = Colors.Transparent };
                label.SetBinding(Label.TextProperty, new Binding(nameof(DisplayItem.Name), BindingMode.TwoWay));
                View = label;

            }

        }

        public class ListViewGroupTemplate : ViewCell
        {

            public ListViewGroupTemplate()
            {
                var label = new Label { VerticalTextAlignment = TextAlignment.Center, TextColor = Colors.White, FontAttributes = FontAttributes.Italic, BackgroundColor = Colors.Transparent };
                label.SetBinding(Label.TextProperty, new Binding(nameof(GroupedDisplayItem.Name), BindingMode.TwoWay));
                View = label;
            }

        }
    }
}
