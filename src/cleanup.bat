@echo on
FOR /F "delims=*" %%G IN ('DIR bin* /B /A:D /S') DO RMDIR "%%G" /S /Q 
FOR /F "delims=*" %%G IN ('DIR obj* /B /A:D /S') DO RMDIR "%%G" /S /Q 
FOR /F "delims=*" %%G IN ('DIR testresults* /B /A:D /S') DO RMDIR "%%G" /S /Q 
DEL *.log /Q /S
DEL *.tmp /Q /S
DEL *.pdb /Q /S
DEL *.cache /Q /S
