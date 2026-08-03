• Role: parse Shabbat agenda PDFs → teaching PDF + private MD
• Hosts: Cli (manual/batch), Functions (prod blob trigger)
• Never merge into Api/
• Skip *-teaching.pdf on triggers
• No committed connection strings
• Prefer dotnet test + CLI dry-run before deploy