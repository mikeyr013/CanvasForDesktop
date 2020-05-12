using Windows.Data.Json;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CanvasForDesktop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CoursePage : Page
    {
        public CoursePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            var courses = e.Parameter as JsonArray;
            TextBlock[] textBoxes = {Course1, Course2, Course3, Course4, Course5, Course6};

            for(uint i=0; i<courses.Count; i++)
            {
                if(i > textBoxes.Length) {
                    break;
                }
                else {
                    textBoxes[i].Text = courses.GetObjectAt(i)["name"].GetString();
                }
            }
        }

    }

}
