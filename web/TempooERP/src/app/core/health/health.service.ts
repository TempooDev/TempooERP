import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map, of } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class HealthService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/health';

  check(): Observable<boolean> {
    return this.http.get<{ ok: boolean }>(this.baseUrl).pipe(
      map(res => res.ok),
      catchError(() => of(false))
    );
  }
}
