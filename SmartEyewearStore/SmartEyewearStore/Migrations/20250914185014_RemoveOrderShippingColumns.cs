using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEyewearStore.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOrderShippingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DECLARE
  v_schema VARCHAR2(128) := SYS_CONTEXT('USERENV','CURRENT_SCHEMA');

  PROCEDURE drop_col_if_exists(p_col IN VARCHAR2) IS
    v_cnt NUMBER;
  BEGIN
    SELECT COUNT(*) INTO v_cnt
    FROM ALL_TAB_COLS
    WHERE OWNER = v_schema AND TABLE_NAME = 'ORDERS' AND COLUMN_NAME = p_col;

    IF v_cnt > 0 THEN
      -- Drop constraints referencing this column (CHECK/UNIQUE/FOREIGN)
      FOR c IN (
        SELECT c.CONSTRAINT_NAME
        FROM USER_CONSTRAINTS c
        JOIN USER_CONS_COLUMNS cc ON c.CONSTRAINT_NAME = cc.CONSTRAINT_NAME
        WHERE c.TABLE_NAME = 'ORDERS'
          AND cc.COLUMN_NAME = p_col
          AND c.CONSTRAINT_TYPE IN ('C','U','R')
      ) LOOP
        EXECUTE IMMEDIATE 'ALTER TABLE '||v_schema||'.ORDERS DROP CONSTRAINT '||c.CONSTRAINT_NAME;
      END LOOP;

      EXECUTE IMMEDIATE 'ALTER TABLE '||v_schema||'.ORDERS DROP COLUMN '||p_col;
    END IF;
  END;
BEGIN
  drop_col_if_exists('SHIPTODIFFERENT');
  drop_col_if_exists('SHIPPINGFULLNAME');
  drop_col_if_exists('SHIPPINGPHONE');
  drop_col_if_exists('SHIPPINGADDRESS1');
  drop_col_if_exists('SHIPPINGADDRESS2');
  drop_col_if_exists('SHIPPINGCITY');
  drop_col_if_exists('SHIPPINGSTATE');
  drop_col_if_exists('SHIPPINGPOSTALCODE');
  drop_col_if_exists('SHIPPINGCOUNTRY');
END;
");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SHIPPINGADDRESS1",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SHIPPINGADDRESS2",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SHIPPINGCITY",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SHIPPINGCOUNTRY",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SHIPPINGFULLNAME",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SHIPPINGPHONE",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SHIPPINGPOSTALCODE",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SHIPPINGSTATE",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SHIPTODIFFERENT",
                schema: "DBS311_252NAA12",
                table: "ORDERS",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: 0);
        }
    }
}
