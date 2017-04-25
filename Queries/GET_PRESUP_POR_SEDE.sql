CREATE PROCEDURE GET_PRESUP_POR_SEDE (VAR_ID_SEDE IN NUMBER,CUR_RPTA OUT sys_refcursor)IS
BEGIN

    SAVEPOINT obtenerResultados;
    
    OPEN CUR_RPTA FOR
        SELECT 
        id_presupuesto,
        fecha_reg,
        usuario_reg,
        ult_modif_fec,
        ult_modif_user,
        fecha_val_ini,
        fecha_val_fin,
        est_actual,
        nomb_presup,
        0 AS MONTO
        FROM
        t_presup;
        
    EXCEPTION 
        WHEN OTHERS THEN
        
            ROLLBACK TO obtenerResultados;
            RAISE;
END;