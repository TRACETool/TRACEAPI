namespace SecurityWebhook.Lib.Repository.Triggers
{
    public class TriggerQueries
    {
        internal const string CreateForbidTampering = @"CREATE OR REPLACE FUNCTION prevent_log_update_or_delete() RETURNS TRIGGER AS $$
            BEGIN
                RAISE EXCEPTION 'Log entries cannot be updated or deleted';
                RETURN NULL;
            END;
            $$ LANGUAGE plpgsql;

            CREATE TRIGGER prevent_log_modification
            BEFORE UPDATE OR DELETE ON ""ImmutableServiceLogs""
            FOR EACH ROW EXECUTE FUNCTION prevent_log_update_or_delete();";

        internal const string DropforbidTampering = @"
            DROP TRIGGER IF EXISTS prevent_log_modification ON ""LogEntries"";
            DROP FUNCTION IF EXISTS prevent_log_update_or_delete();
        ";

        internal const string CreateScanForbidding = @"
            CREATE TRIGGER prevent_scan_modification
            BEFORE UPDATE OR DELETE ON ""ScanDetails""
            FOR EACH ROW EXECUTE FUNCTION prevent_log_update_or_delete();";

        internal const string DropScanForbiding = @"
            DROP TRIGGER IF EXISTS prevent_scan_modification ON ""ScanDetails"";
            DROP FUNCTION IF EXISTS prevent_log_update_or_delete();
        ";
        internal const string CreateVulnerabilitiesForbidding = @"
            CREATE TRIGGER prevent_vuln_modification
            BEFORE UPDATE OR DELETE ON ""Vulnerabilities""
            FOR EACH ROW EXECUTE FUNCTION prevent_log_update_or_delete();";

        internal const string DropVulnerabilitiesForbiding = @"
            DROP TRIGGER IF EXISTS prevent_vuln_modification ON ""Vulnerabilities"";
            DROP FUNCTION IF EXISTS prevent_log_update_or_delete();
        ";
        internal const string CreateAnomaliesForbidding = @"
            CREATE TRIGGER prevent_anomalies_modification
            BEFORE UPDATE OR DELETE ON ""Anomalies""
            FOR EACH ROW EXECUTE FUNCTION prevent_log_update_or_delete();";

        internal const string DropAnomaliesForbiding = @"
            DROP TRIGGER IF EXISTS prevent_anomalies_modification ON ""Anomalies"";
            DROP FUNCTION IF EXISTS prevent_log_update_or_delete();
        ";
    }
}
