using RaceDay.Api.Models;
using Xunit;
namespace RaceDay.Api.Tests;
public class BasicModelTests
{
    [Theory]
    [InlineData("Organiser")]
    [InlineData("Participant")]
    public void User_can_represent_supported_roles(string role)
    { var user = new AppUser { FirstName="Test", LastName="User", Email="test@example.com", PasswordHash="hashed", Role=role }; Assert.Equal(role,user.Role); }
    [Theory]
    [InlineData("run")]
    [InlineData("walk")]
    [InlineData("cycle")]
    public void Event_type_values_match_the_assessment(string type)
    { var ev = new RaceEvent { EventName="Cape Town Road Race", Location="Cape Town", EventType=type, Distance=10 }; Assert.Contains(ev.EventType,new[]{"run","walk","cycle"}); }
    [Fact]
    public void Enrolment_links_participant_event_and_category()
    { var e = new Enrolment { ParticipantId=3, EventId=5, CategoryId=2 }; Assert.Equal(3,e.ParticipantId); Assert.Equal(5,e.EventId); Assert.Equal(2,e.CategoryId); }
}