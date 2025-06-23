-- Create PromptFlow and FlowExecution tables manually

-- Create PromptFlows table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PromptFlows' AND xtype='U')
BEGIN
    CREATE TABLE [PromptFlows] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NULL,
        [Version] nvarchar(20) NOT NULL,
        [UserId] nvarchar(100) NULL,
        [Tags] nvarchar(1000) NULL,
        [FlowData] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_PromptFlows] PRIMARY KEY ([Id])
    );

    -- Create indexes for PromptFlows
    CREATE INDEX [IX_PromptFlows_Name] ON [PromptFlows] ([Name]);
    CREATE INDEX [IX_PromptFlows_UserId] ON [PromptFlows] ([UserId]);
    CREATE INDEX [IX_PromptFlows_CreatedAt] ON [PromptFlows] ([CreatedAt]);
END

-- Create FlowExecutions table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FlowExecutions' AND xtype='U')
BEGIN
    CREATE TABLE [FlowExecutions] (
        [Id] uniqueidentifier NOT NULL,
        [FlowId] uniqueidentifier NOT NULL,
        [InputVariables] nvarchar(max) NOT NULL,
        [OutputResult] nvarchar(max) NULL,
        [ExecutionTime] bigint NOT NULL,
        [Status] nvarchar(20) NOT NULL,
        [ErrorMessage] nvarchar(2000) NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_FlowExecutions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FlowExecutions_PromptFlows_FlowId] FOREIGN KEY ([FlowId]) REFERENCES [PromptFlows] ([Id]) ON DELETE CASCADE
    );

    -- Create indexes for FlowExecutions
    CREATE INDEX [IX_FlowExecutions_FlowId] ON [FlowExecutions] ([FlowId]);
    CREATE INDEX [IX_FlowExecutions_CreatedAt] ON [FlowExecutions] ([CreatedAt]);
    CREATE INDEX [IX_FlowExecutions_Status] ON [FlowExecutions] ([Status]);
END

-- Check what tables exist now
SELECT 
    TABLE_NAME,
    'Table exists' as Status
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
    AND TABLE_NAME IN ('Collections', 'PromptTemplates', 'PromptVariables', 'PromptExecutions', 'VariableCollections', 'PromptFlows', 'FlowExecutions')
ORDER BY TABLE_NAME;
