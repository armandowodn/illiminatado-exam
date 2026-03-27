import { FormsModule } from '@angular/forms';
import { NgZone } from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { UploadService } from '../../../../services/upload.service';


import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-upload',
  standalone: true,
  imports: [CommonModule, HttpClientModule,FormsModule],
  templateUrl: './upload.component.html'
})
export class UploadComponent {

  selectedFile: File | null = null;
  payrollNo: number = 67008;
  loading = false;
  message = '';

  constructor(private uploadService: UploadService,private http: HttpClient) {}

  onFileSelected(event: any) {
    const file = event.target.files[0];

    if (!file) return;

    const validTypes = [
      'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      'application/vnd.ms-excel'
    ];

    if (!validTypes.includes(file.type)) {
      alert('Invalid file type. Please upload Excel file only.');
      return;
    }

    this.selectedFile = file;
    this.message = '';
  }

  exportData() {
    if (!this.payrollNo) {
      this.message = 'Please enter Payroll Number.';
      return;
    }

    this.loading = true;

    //this.http.get(`https://localhost:44397/api/export/payroll/${this.payrollNo}`, {
    this.http.get(`https://localhost:44397/api/Upload/export/${this.payrollNo}`, {
      responseType: 'blob'
    })
    .pipe(
      finalize(() => this.loading = false)
    )
    .subscribe({
      next: (file) => {
        const url = window.URL.createObjectURL(file);
        const a = document.createElement('a');
        a.href = url;
        a.download = `Payroll_${this.payrollNo}.xlsx`;
        a.click();
        window.URL.revokeObjectURL(url);

        this.message = 'Export success!';
      },
      error: () => {
        this.message = 'Export failed.';
      }
    });
  }

  upload() {
  console.log('UPLOAD CLICKED');

  if (!this.selectedFile) {
    this.message = 'Please select an Excel file.';
    return;
  }

  this.loading = true;

  const formData = new FormData();
  formData.append('file', this.selectedFile);

  this.http.post<any>(
    'https://localhost:44397/api/Upload/upload',
    formData
  )
  .pipe(
    finalize(() => {
      this.loading = false;
      console.log('FINALIZE FIRED');
      
    })
  )
  .subscribe({
    next: (res) => {
      console.log('SUCCESS:', res);
      this.message = `Upload success! Rows inserted: ${res.count}`;
      alert('Success')
    },
    error: (err) => {
      console.error(err);
      this.message = 'Upload failed.';
    }
  });
}

  clear() {
    this.selectedFile = null;
    this.message = '';
  }
}