IF ('$(Environment)' = 'Development') BEGIN
  PRINT 'Running development post-deployment scripts'
  :r ".\Scripts\Users\Development.sql"
END