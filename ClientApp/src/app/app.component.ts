import { Component, OnInit, ViewEncapsulation, OnDestroy } from '@angular/core';

/**
 * Application entry point
 */
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit, OnDestroy {


  /**
   * Create an instance of {@link AppComponent}
   */
  constructor(
  ) { }

  /**
   * Angular ngOnInit function
   */
  public ngOnInit(): void {
    const vm = this;
  }

  /**
   * Angular ngOnDestroy function
   */
  ngOnDestroy(): void {
  }
}
