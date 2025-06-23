-- Fix migration history for existing database
-- This script manually inserts migration history records for tables that already exist

-- Check if __EFMigrationsHistory table exists, create if not
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='__EFMigrationsHistory' AND xtype='U')
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END

-- Mark initial migration as applied (since tables already exist)
IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20250602023955_InitialSqlServerMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES ('20250602023955_InitialSqlServerMigration', '9.0.1');
END

-- Check what tables exist
SELECT 
    TABLE_NAME,
    'Table exists' as Status
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
    AND TABLE_NAME IN ('Collections', 'PromptTemplates', 'PromptVariables', 'PromptExecutions', 'VariableCollections', 'PromptFlows', 'FlowExecutions')
ORDER BY TABLE_NAME;
