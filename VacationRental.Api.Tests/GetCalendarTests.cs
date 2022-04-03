using System;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Api.DTO.Requests;
using VacationRental.Api.DTO.Responses;
using VacationRental.Api.DTO.Responses.Calendar;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class GetCalendarTests
    {
        private readonly HttpClient _client;

        public GetCalendarTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenGetCalendar_ThenAGetReturnsTheCalculatedCalendar()
        {
            var postRentalRequest = new RentalRequest
            {
                Units = 2
            };

            ResourceIdResponse postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadAsAsync<ResourceIdResponse>();
            }

            var postBooking1Request = new BookingRequest
            {
                 RentalId = postRentalResult.Id,
                 Nights = 2,
                 Start = new DateTime(2000, 01, 02)
            };

            ResourceIdResponse postBooking1Result;
            using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
            {
                Assert.True(postBooking1Response.IsSuccessStatusCode);
                postBooking1Result = await postBooking1Response.Content.ReadAsAsync<ResourceIdResponse>();
            }

            var postBooking2Request = new BookingRequest
            {
                RentalId = postRentalResult.Id,
                Nights = 2,
                Start = new DateTime(2000, 01, 03)
            };

            ResourceIdResponse postBooking2Result;
            using (var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request))
            {
                Assert.True(postBooking2Response.IsSuccessStatusCode);
                postBooking2Result = await postBooking2Response.Content.ReadAsAsync<ResourceIdResponse>();
            }

            using (var getCalendarResponse = await _client.GetAsync($"/api/v1/calendar?rentalId={postRentalResult.Id}&start=2000-01-01&nights=5"))
            {
                Assert.True(getCalendarResponse.IsSuccessStatusCode);

                var getCalendarResult = await getCalendarResponse.Content.ReadAsAsync<CalendarResponse>();
                
                Assert.Equal(postRentalResult.Id, getCalendarResult.RentalId);
                Assert.Equal(5, getCalendarResult.Dates.Count);

                Assert.Equal(new DateTime(2000, 01, 01), getCalendarResult.Dates[0].Date);
                Assert.Empty(getCalendarResult.Dates[0].Bookings);
                
                Assert.Equal(new DateTime(2000, 01, 02), getCalendarResult.Dates[1].Date);
                Assert.Single(getCalendarResult.Dates[1].Bookings);
                Assert.Contains(getCalendarResult.Dates[1].Bookings, x => x.Id == postBooking1Result.Id);
                
                Assert.Equal(new DateTime(2000, 01, 03), getCalendarResult.Dates[2].Date);
                Assert.Equal(2, getCalendarResult.Dates[2].Bookings.Count);
                Assert.Contains(getCalendarResult.Dates[2].Bookings, x => x.Id == postBooking1Result.Id);
                Assert.Contains(getCalendarResult.Dates[2].Bookings, x => x.Id == postBooking2Result.Id);
                
                Assert.Equal(new DateTime(2000, 01, 04), getCalendarResult.Dates[3].Date);
                Assert.Single(getCalendarResult.Dates[3].Bookings);
                Assert.Contains(getCalendarResult.Dates[3].Bookings, x => x.Id == postBooking2Result.Id);
                
                Assert.Equal(new DateTime(2000, 01, 05), getCalendarResult.Dates[4].Date);
                Assert.Empty(getCalendarResult.Dates[4].Bookings);
            }
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenGetCalendar_ThenAGetReturnsTheCalculatedCalendarWithPrepDays()
        {
            var postRentalRequest = new RentalRequest
            {
                Units = 2,
                PreparationTimeInDays = 3
            };

            ResourceIdResponse postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadAsAsync<ResourceIdResponse>();
            }

            var postBooking1Request = new BookingRequest
            {
                RentalId = postRentalResult.Id,
                Nights = 2,
                Start = new DateTime(2000, 01, 02)
            };

            ResourceIdResponse postBooking1Result;
            using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
            {
                Assert.True(postBooking1Response.IsSuccessStatusCode);
                postBooking1Result = await postBooking1Response.Content.ReadAsAsync<ResourceIdResponse>();
            }

            var postBooking2Request = new BookingRequest
            {
                RentalId = postRentalResult.Id,
                Nights = 2,
                Start = new DateTime(2000, 01, 03)
            };

            ResourceIdResponse postBooking2Result;
            using (var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request))
            {
                Assert.True(postBooking2Response.IsSuccessStatusCode);
                postBooking2Result = await postBooking2Response.Content.ReadAsAsync<ResourceIdResponse>();
            }

            using (var getCalendarResponse = await _client.GetAsync($"/api/v1/calendar?rentalId={postRentalResult.Id}&start=2000-01-01&nights=10"))
            {
                Assert.True(getCalendarResponse.IsSuccessStatusCode);

                var getCalendarResult = await getCalendarResponse.Content.ReadAsAsync<CalendarResponse>();

                Assert.Equal(postRentalResult.Id, getCalendarResult.RentalId);
                Assert.Equal(10, getCalendarResult.Dates.Count);

                Assert.Equal(new DateTime(2000, 01, 01), getCalendarResult.Dates[0].Date);
                Assert.Empty(getCalendarResult.Dates[0].Bookings);

                Assert.Equal(new DateTime(2000, 01, 02), getCalendarResult.Dates[1].Date);
                Assert.Single(getCalendarResult.Dates[1].Bookings);
                Assert.Contains(getCalendarResult.Dates[1].Bookings, x => x.Id == postBooking1Result.Id && x.Unit == 1);

                Assert.Equal(new DateTime(2000, 01, 03), getCalendarResult.Dates[2].Date);
                Assert.Equal(2, getCalendarResult.Dates[2].Bookings.Count);
                Assert.Contains(getCalendarResult.Dates[2].Bookings, x => x.Id == postBooking1Result.Id && x.Unit == 1);
                Assert.Contains(getCalendarResult.Dates[2].Bookings, x => x.Id == postBooking2Result.Id && x.Unit == 2);

                Assert.Equal(new DateTime(2000, 01, 04), getCalendarResult.Dates[3].Date);
                Assert.Single(getCalendarResult.Dates[3].Bookings);
                Assert.Contains(getCalendarResult.Dates[3].Bookings, x => x.Id == postBooking2Result.Id && x.Unit == 2);
                Assert.Contains(getCalendarResult.Dates[3].PreparationTimes, x => x.Unit == 1);

                Assert.Equal(new DateTime(2000, 01, 05), getCalendarResult.Dates[4].Date);
                Assert.Contains(getCalendarResult.Dates[4].PreparationTimes, x => x.Unit == 1);
                Assert.Contains(getCalendarResult.Dates[4].PreparationTimes, x => x.Unit == 2);

                Assert.Equal(new DateTime(2000, 01, 06), getCalendarResult.Dates[5].Date);
                Assert.Contains(getCalendarResult.Dates[5].PreparationTimes, x => x.Unit == 1);
                Assert.Contains(getCalendarResult.Dates[5].PreparationTimes, x => x.Unit == 2);

                Assert.Equal(new DateTime(2000, 01, 07), getCalendarResult.Dates[6].Date);
                Assert.Contains(getCalendarResult.Dates[6].PreparationTimes, x => x.Unit == 2);
            }
        }

        [Fact]
        public async Task GivenSeveralBookings_WhenGetCalendar_AllocatesRightUnit()
        {
            var postRentalRequest = new RentalRequest
            {
                Units = 2,
                PreparationTimeInDays = 1
            };

            ResourceIdResponse postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadAsAsync<ResourceIdResponse>();
            }

            var postBooking1Request = new BookingRequest
            {
                RentalId = postRentalResult.Id,
                Nights = 2,
                Start = new DateTime(2000, 01, 01)
            };

            ResourceIdResponse postBooking1Result;
            using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
            {
                Assert.True(postBooking1Response.IsSuccessStatusCode);
                postBooking1Result = await postBooking1Response.Content.ReadAsAsync<ResourceIdResponse>();
            }

            var postBooking2Request = new BookingRequest
            {
                RentalId = postRentalResult.Id,
                Nights = 10,
                Start = new DateTime(2000, 01, 01)
            };

            ResourceIdResponse postBooking2Result;
            using (var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request))
            {
                Assert.True(postBooking2Response.IsSuccessStatusCode);
                postBooking2Result = await postBooking2Response.Content.ReadAsAsync<ResourceIdResponse>();
            }

            var postBooking3Request = new BookingRequest
            {
                RentalId = postRentalResult.Id,
                Nights = 5,
                Start = new DateTime(2000, 01, 05)
            };

            ResourceIdResponse postBooking3Result;
            using (var postBooking3Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking3Request))
            {
                Assert.True(postBooking3Response.IsSuccessStatusCode);
                postBooking3Result = await postBooking3Response.Content.ReadAsAsync<ResourceIdResponse>();
            }

            using (var getCalendarResponse = await _client.GetAsync($"/api/v1/calendar?rentalId={postRentalResult.Id}&start=2000-01-01&nights=10"))
            {
                Assert.True(getCalendarResponse.IsSuccessStatusCode);

                var getCalendarResult = await getCalendarResponse.Content.ReadAsAsync<CalendarResponse>();

                Assert.Equal(new DateTime(2000, 01, 01), getCalendarResult.Dates[0].Date);
                Assert.Equal(2, getCalendarResult.Dates[0].Bookings.Count);
                Assert.Contains(getCalendarResult.Dates[0].Bookings, x => x.Id == postBooking1Result.Id && x.Unit == 1);
                Assert.Contains(getCalendarResult.Dates[0].Bookings, x => x.Id == postBooking2Result.Id && x.Unit == 2);

                Assert.Equal(new DateTime(2000, 01, 05), getCalendarResult.Dates[4].Date);
                Assert.Equal(2, getCalendarResult.Dates[4].Bookings[0].Unit);
                Assert.Equal(1, getCalendarResult.Dates[4].Bookings[1].Unit);
            }
        }
    }
}
