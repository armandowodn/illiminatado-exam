import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'upload',
    pathMatch: 'full'
  },
  {
    path: 'upload',
    loadComponent: () =>
      import('./modules/upload/pages/upload/upload.component')
        .then(m => m.UploadComponent)
  }
];