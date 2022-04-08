@echo off

for %%d in (bin obj) do (for /f %%f in ('dir /s /b %%d') do (rmdir /s /q %%f))

pause