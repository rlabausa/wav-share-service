ALTER ROLE [db_owner] ADD MEMBER [svc_tester];


GO
ALTER ROLE [db_accessadmin] ADD MEMBER [svc_tester];


GO
ALTER ROLE [db_securityadmin] ADD MEMBER [svc_tester];


GO
ALTER ROLE [db_ddladmin] ADD MEMBER [svc_tester];


GO
ALTER ROLE [db_backupoperator] ADD MEMBER [svc_tester];


GO
ALTER ROLE [db_datareader] ADD MEMBER [svc_tester];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [svc_tester];


GO
ALTER ROLE [db_denydatareader] ADD MEMBER [svc_tester];


GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [svc_tester];

