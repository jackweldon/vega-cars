import { ToastyService } from 'ng2-toasty';
import { ErrorHandler, Inject, NgZone, isDevMode } from "@angular/core";
import * as Raven from 'raven-js';

export class AppErrorHandler implements ErrorHandler {

    constructor(
        private ngZone: NgZone,
        @Inject(ToastyService) private toastySerice: ToastyService) {

    }

    handleError(error: any): void {
        if (!isDevMode())
            Raven.captureException(error.originalError || error);
        else
            throw error;

        this.ngZone.run(() => {
            this.toastySerice.error({
                title: 'Error',
                msg: ' An unexpected error occured',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            })
        })
    }
}