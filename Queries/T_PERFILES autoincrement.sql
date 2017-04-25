CREATE SEQUENCE T_PERFILES_AI START WITH 1;

CREATE OR REPLACE TRIGGER T_PERFILES_AI_TRIG
BEFORE INSERT ON T_PERFILES
FOR EACH ROW
BEGIN
    
    SELECT T_PERFILES_AI.NEXTVAL
    INTO :new.ID_PERFIL
    FROM dual;

END;