import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../shared/services/account.service';
import { map } from 'rxjs/operators';

export const loggedOutGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);

  return accountService.checkToken().pipe(
    map(isValid => {
      if (isValid) {
        router.navigate(['/home']);
        return false;
      }
      return true;
    })
  );
};
