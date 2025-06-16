var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LivingMessiah>("livingmessiah");

builder.AddProject<Projects.LivingMessiahAdmin>("livingmessiahadmin");

builder.Build().Run();
