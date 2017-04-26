CREATE or replace PROCEDURE GET_PRESUP_POR_SEDE (VAR_ID_SEDE IN NUMBER,CUR_RPTA OUT sys_refcursor)IS
BEGIN

    SAVEPOINT obtenerResultados;
    
    OPEN CUR_RPTA FOR
        SELECT 
        A.id_presupuesto,
        A.fecha_reg,
        A.usuario_reg,
        A.ult_modif_fec,
        A.ult_modif_user,
        A.fecha_val_ini,
        A.fecha_val_fin,
        A.est_actual,
        A.nomb_presup,
        0 AS MONTO
        FROM
        t_presup A ;
        
    EXCEPTION 
        WHEN OTHERS THEN
        
            ROLLBACK TO obtenerResultados;
            RAISE;
END;