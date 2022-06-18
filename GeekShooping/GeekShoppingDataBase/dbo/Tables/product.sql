CREATE TABLE [dbo].[product] (
    [id]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [name]          NVARCHAR (150)  NOT NULL,
    [price]         DECIMAL (18, 2) NOT NULL,
    [description]   NVARCHAR (500)  NULL,
    [category_name] NVARCHAR(100) NOT NULL,
    [image_url]     NVARCHAR (300)  NOT NULL,
    CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED ([id] ASC)
);

