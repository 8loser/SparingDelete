#參數說明

```
  -f, --folder       Required. 起始目錄路徑  
  -p, --pattern      (Default: *.bak) 搜尋名稱樣本  
  -k, --keep         (Default: 5) 保留檔案數量  
  -r, --recycling    (Default: False) 是否移至資源回收筒  
  -l, --log          (Default: False) 是否產生紀錄檔  
  --help             Display this help screen.
```

#Example
```
LogClear.exe -f C:\log
```
刪除 **C:\log** 資料夾內副檔名為 **.bak** 的檔案，並保留最新的5個。
```
LogClear.exe -f C:\log -p *.log
```
刪除 **C:\log** 資料夾內副檔名為 **.log** 的檔案，並保留最新的5個。
```
LogClear.exe -f C:\log -p ??.bak
```
刪除 **C:\log** 資料夾內檔名為 **兩個字母** 且副檔名為 **.bak** 的檔案，並保留最新的5個。
```
LogClear.exe -f C:\log -k 10
```
刪除 **C:\log** 資料夾內副檔名為 .bak 的檔案，並保留最新的 **10** 個。
```
LogClear.exe -f C:\log -r
```
將 **C:\log** 資料夾內副檔名為 .bak 的檔案 **移至資源回收筒**，並保留最新的5個。
```
LogClear.exe -f C:\log -l
```
將 **C:\log** 資料夾內副檔名為 .bak 的檔案移至資源回收筒，並保留最新的5個，且 **產生紀錄檔**。
