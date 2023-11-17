import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthFacade } from '../../../core/facades/auth/auth-facade.service';

export const accessGuard: CanActivateFn = (route, state) => {
  const authFacade = inject(AuthFacade);
  const router = inject(Router);

  if (!authFacade.getAccessToken()) {
    router.navigateByUrl('auth/login');
    return false;
  }

  return true;
};