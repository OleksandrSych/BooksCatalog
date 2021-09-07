import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { Book } from '../book/book';

@Injectable()
export class ExcelService {
  constructor(private http: HttpClient
  ) {
  }
  exportexcel(books: any[], fileName: string): void {
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(books);
    ws["!cols"] = this.formatExcelCols(books);
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Books');

    /* save to file */
    XLSX.writeFile(wb, fileName);

  }
  formatExcelCols(json: any[]) {
    let widthArr = Object.keys(json[0]).map(key => {
      return { width: key.length + 2 } // plus 2 to account for short object keys
    })
    for (let i = 0; i < json.length; i++) {
      let value = Object.values(json[i]);
      for (let j = 0; j < value.length; j++) {
        if (value[j] !== null && value[j].toString().length > widthArr[j].width) {
          widthArr[j].width = value[j].toString().length + 2;
        }
      }
    }
    return widthArr
  }

  excelToObjArray(binaryString: string): any[] {
    /* read workbook */ 
    const wb: XLSX.WorkBook = XLSX.read(binaryString, { type: 'binary' });

    /* grab first sheet */
    const wsname: string = wb.SheetNames[0];
    const ws: XLSX.WorkSheet = wb.Sheets[wsname];

    /* save data */
    return XLSX.utils.sheet_to_json(ws);
  }
    
}
