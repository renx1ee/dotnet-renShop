using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Application.Services;

public class NotificationService
{
    private readonly ILogger<NotificationService> logger;
    private readonly IReviewRepository reviewRepository;
    
    public NotificationService(
        ILogger<NotificationService> logger,
        IReviewRepository reviewRepository)
    {
        this.logger = logger;
        this.reviewRepository = reviewRepository;
    }
    
    public async Task NotifyModerators(Review review)
    {
        try
        {
            logger.LogInformation("NotifyModerators method is starting!");
            
            
            
            
        }
        catch (Exception ex)
        {
            logger.LogError($"Error notify moderators bout review: {review}, error message: {ex.Message}");
        }
        logger.LogInformation("NotifyModerators method is finished!");
    }
}