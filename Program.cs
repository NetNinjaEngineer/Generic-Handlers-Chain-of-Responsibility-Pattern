using ApplyingGenericRepositoryPattern.Data;
using ApplyingGenericRepositoryPattern.Handlers.Helpers;
using ApplyingGenericRepositoryPattern.Handlers.Implementors;

namespace Handlers;

internal class Program
{
    static async Task Main(string[] args)
    {
        using (var context = new ApplicationDbContext())
        {
            var provider = new GPAProvider(context);

            var studentExistenceAndEnrolledHandler = new StudentExistenceAndEnrolledHandler(context);
            var studentGpaExistenceHandler = new StudentGpaExistenceHandler(context, provider);
            var gpaBetweenAllowedRangeHandler = new GPABetweenAllowedRangeHandler(context, provider);
            var gpaExceededAllowedRangeHandler = new GPAExceededAllowedRangeHandler(context, provider);

            studentExistenceAndEnrolledHandler.SetNext(studentGpaExistenceHandler);
            studentGpaExistenceHandler.SetNext(gpaBetweenAllowedRangeHandler);
            gpaBetweenAllowedRangeHandler.SetNext(gpaExceededAllowedRangeHandler);

            var response = await studentExistenceAndEnrolledHandler.HandleAsync(new SuggestCoursesRequest()
            {
                StudentId = 5
            });

            await Console.Out.WriteLineAsync(string.Join(", ", response.SuggestionCourses));
        }

        Console.ReadKey();
    }
}
