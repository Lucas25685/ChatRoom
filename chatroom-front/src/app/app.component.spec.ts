import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AppInsightsService } from './_common/services/app-insights/app-insights.service';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        AppComponent,
        HttpClientTestingModule,
      ],
      providers: [
        AppComponent, { provide: AppInsightsService, useValue: {
          loadAppInsights: jasmine.createSpy('loadAppInsights'),
          trackPageView: jasmine.createSpy('trackPageView'),
          trackEvent: jasmine.createSpy('trackEvent')
        }}
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });
});
