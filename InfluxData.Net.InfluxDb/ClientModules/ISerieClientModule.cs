﻿using System.Collections.Generic;
using System.Threading.Tasks;
using InfluxData.Net.Common.Infrastructure;
using InfluxData.Net.InfluxDb.Models.Responses;
using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb.ClientSubModules;

namespace InfluxData.Net.InfluxDb.ClientModules
{
    public interface ISerieClientModule
    {
        /// <summary>
        /// Gets distinct series.
        /// </summary>
        /// <param name="dbName">Database name.</param>
        /// <param name="measurementName">Measurement name (optional).</param>
        /// <param name="filters">A collection of "WHERE" clause filters (optional).</param>
        /// <returns></returns>
        Task<IEnumerable<SerieSet>> GetSeriesAsync(string dbName, string measurementName = null, IEnumerable<string> filters = null);

        /// <summary>
        /// Deletes all data points from a serie.
        /// </summary>
        /// <param name="dbName">Database name.</param>
        /// <param name="measurementName">Measurement name.</param>
        /// <param name="filters">A collection of "WHERE" clause filters (optional).</param>
        /// <returns></returns>
        Task<IInfluxDataApiResponse> DropSeriesAsync(string dbName, string measurementName, IEnumerable<string> filters = null);

        /// <summary>
        /// Deletes all data points from multiple series.
        /// </summary>
        /// <param name="dbName">Database name.</param>
        /// <param name="measurementName">A list of measurement names.</param>
        /// <param name="filters">A collection of "WHERE" clause filters (optional).</param>
        /// <returns></returns>
        Task<IInfluxDataApiResponse> DropSeriesAsync(string dbName, IEnumerable<string> measurementNames, IEnumerable<string> filters = null);

        /// <summary>
        /// Gets distinct measurements.
        /// </summary>
        /// <param name="dbName">Database name.</param>
        /// <param name="filters">A collection of "WHERE" clause filters (optional).</param>
        /// <returns></returns>
        Task<IEnumerable<Measurement>> GetMeasurementsAsync(string dbName, IEnumerable<string> filters = null);

        /// <summary>
        /// Deletes all data points and series itself. Unlike DROP SERIES it also deletes
        /// the measurement from the index.
        /// </summary>
        /// <param name="dbName">Database name.</param>
        /// <param name="measurementName">Measurement name.</param>
        /// <returns></returns>
        Task<IInfluxDataApiResponse> DropMeasurementAsync(string dbName, string measurementName);

        /// <summary>
        /// Creates a BatchWriter instance which can then be shared by multiple threads/processes to be used
        /// for batch Point writing. It will keep the points in-memory for a specified interval. After the 
        /// interval times out, the collection will get dequeued and batch-written to influx. The BatchWriter
        /// will keep checking the collection after each interval until stopped.
        /// </summary>
        /// <see cref="http://www.codethinked.com/blockingcollection-and-iproducerconsumercollection"/>
        /// <param name="dbName">Database name.</param>
        /// <param name="retenionPolicy">Retention policy.</param>
        /// <param name="precision">Precision.</param>
        /// <returns>BatchWriter instance.</returns>
        IBatchWriter CreateBatchWriter(string dbName, string retenionPolicy = "default", TimeUnit precision = TimeUnit.Milliseconds);
    }
}