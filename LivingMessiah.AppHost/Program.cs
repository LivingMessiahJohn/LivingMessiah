var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LivingMessiah>("livingmessiah");

builder.Build().Run();
