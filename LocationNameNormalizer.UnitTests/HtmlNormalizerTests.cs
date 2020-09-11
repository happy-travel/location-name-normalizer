using HtmlAgilityPack;
using LocationNameNormalizer.Extensions;
using Xunit;

namespace LocationNameNormalizer.UnitTests
{
    public class HtmlNormalizerTests
    {
        [Fact]
        public void Html_normalization_should_remove_attributes()
        {
            var html = @"<p ar=""jkjl""><b>Property Location</b> <br />With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from Al Wahda Mall and Caracal Shooting Club. This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>";

            var outerHtml = @"<p><b>Property Location</b> <br>With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from Al Wahda Mall and Caracal Shooting Club. This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>";

            var result = html.NormalizeHtml();

            Assert.True(result == outerHtml);
        }


        [Fact]
        public void Html_normalization_should_replace_div_to_br()
        {
            var html = @"<p><b>Property Location</b> <br />With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from <div>Al Wahda Mall and Caracal Shooting Club.</div> This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>";

            var outerHtml = @"<p><b>Property Location</b> <br>With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from <br>Al Wahda Mall and Caracal Shooting Club.<br> This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>";

            var result = html.NormalizeHtml();

            Assert.True(result == outerHtml);
        }
        
        [Fact]
        public void Html_normalization_should_remove_not_needed_tags()
        {
            var html = @"<p><b>Property Location</b> <br />With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from <input>Al Wahda Mall and Caracal Shooting Club.</input> This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>";

            var outerHtml = @"<p><b>Property Location</b> <br>With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from Al Wahda Mall and Caracal Shooting Club. This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>";

            var result = html.NormalizeHtml();

            Assert.True(result == outerHtml);
        }
        
        [Fact]
        public void Html_normalization_should_replace_tags()
        {
            var html = @"<p><b>Property Location</b> <br />With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from <strong>Al Wahda Mall and Caracal Shooting Club.</strong> This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>";

            var outerHtml = @"<p><b>Property Location</b> <br>With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from <b>Al Wahda Mall and Caracal Shooting Club.</b> This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>";

            var result = html.NormalizeHtml();

            Assert.True(result == outerHtml);
        }
        
        [Fact]
        public void Html_normalization_should_process_h1_tags()
        {
            var html = @"<p><h1>Property <i>Location</i></h1> With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from Al Wahda Mall and Caracal Shooting Club. This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>
    <h1><h1>Property <i>Location</i></h1> With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from Al Wahda Mall and Caracal Shooting Club. This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</h1>";

            var outerHtml = @"<p><b>Property <i>Location</i></b><br> With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from Al Wahda Mall and Caracal Shooting Club. This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>
    <h1>Property Location With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from Al Wahda Mall and Caracal Shooting Club. This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</h1>";

            var result = html.NormalizeHtml();
            
            Assert.True(result == outerHtml);
        }
        
        [Fact]
        public void Html_normalization_should_process_br_tags()
        {
            var html = @"<p><br><ol><br>Property Location<br></ol><br><br><br> With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from Al Wahda Mall and Caracal Shooting Club. This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.<br /><br /><br /></p>";

            var outerHtml = @"<p><ul>Property Location</ul> With a stay at Marriott Hotel Downtown, Abu Dhabi, you''ll be centrally located in Abu
    Dhabi, steps from Al Wahda Mall and Caracal Shooting Club. This 5-star hotel is within close proximity of Shaikh
    Khalifa Medical City and Al Nahyan Stadium.</p>";

            var result = html.NormalizeHtml();
            
            Assert.True(result == outerHtml);
        }
    }
}