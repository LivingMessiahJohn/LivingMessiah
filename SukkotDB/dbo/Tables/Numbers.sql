CREATE TABLE [dbo].[Numbers] (
    [Number] BIGINT NULL
);


GO
CREATE UNIQUE CLUSTERED INDEX [CIX_Number]
    ON [dbo].[Numbers]([Number] ASC) WITH (FILLFACTOR = 100, DATA_COMPRESSION = ROW);

