﻿// -----------------------------------------------------------------------
// <copyright file="InvoiceFee.cs" company="Nodine Legal, LLC">
// Licensed to Nodine Legal, LLC under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  Nodine Legal, LLC licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
// </copyright>
// -----------------------------------------------------------------------

namespace OpenLawOffice.Data.DBOs.Billing
{
    using System;
    using AutoMapper;

    [Common.Models.MapMe]
    public class InvoiceFee : Core
    {
        [ColumnMapping(Name = "id")]
        public Guid Id { get; set; }

        [ColumnMapping(Name = "invoice_id")]
        public Guid InvoiceId { get; set; }

        [ColumnMapping(Name = "fee_id")]
        public Guid FeeId { get; set; }

        [ColumnMapping(Name = "amount")]
        public decimal Amount { get; set; }

        [ColumnMapping(Name = "tax_amount")]
        public decimal TaxAmount { get; set; }

        [ColumnMapping(Name = "details")]
        public string Details { get; set; }

        public void BuildMappings()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(InvoiceFee), new ColumnAttributeTypeMapper<InvoiceFee>());
            Mapper.CreateMap<DBOs.Billing.InvoiceFee, Common.Models.Billing.InvoiceFee>()
                .ForMember(dst => dst.IsStub, opt => opt.UseValue(false))
                .ForMember(dst => dst.Created, opt => opt.ResolveUsing(db =>
                {
                    return db.UtcCreated.ToSystemTime();
                }))
                .ForMember(dst => dst.Modified, opt => opt.ResolveUsing(db =>
                {
                    return db.UtcModified.ToSystemTime();
                }))
                .ForMember(dst => dst.Disabled, opt => opt.ResolveUsing(db =>
                {
                    return db.UtcDisabled.ToSystemTime();
                }))
                .ForMember(dst => dst.CreatedBy, opt => opt.ResolveUsing(db =>
                {
                    return new Common.Models.Account.Users()
                    {
                        PId = db.CreatedByUserPId,
                        IsStub = true
                    };
                }))
                .ForMember(dst => dst.ModifiedBy, opt => opt.ResolveUsing(db =>
                {
                    return new Common.Models.Account.Users()
                    {
                        PId = db.ModifiedByUserPId,
                        IsStub = true
                    };
                }))
                .ForMember(dst => dst.DisabledBy, opt => opt.ResolveUsing(db =>
                {
                    if (!db.DisabledByUserPId.HasValue) return null;
                    return new Common.Models.Account.Users()
                    {
                        PId = db.DisabledByUserPId.Value,
                        IsStub = true
                    };
                }))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Invoice, opt => opt.ResolveUsing(db =>
                {
                    return new Common.Models.Billing.Invoice()
                    {
                        Id = db.InvoiceId,
                        IsStub = true
                    };
                }))
                .ForMember(dst => dst.Fee, opt => opt.ResolveUsing(db =>
                {
                    return new Common.Models.Billing.Fee()
                    {
                        Id = db.FeeId,
                        IsStub = true
                    };
                }))
                .ForMember(dst => dst.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dst => dst.TaxAmount, opt => opt.MapFrom(src => src.TaxAmount))
                .ForMember(dst => dst.Details, opt => opt.MapFrom(src => src.Details));

            Mapper.CreateMap<Common.Models.Billing.InvoiceFee, DBOs.Billing.InvoiceFee>()
                .ForMember(dst => dst.UtcCreated, opt => opt.ResolveUsing(db =>
                {
                    return db.Created.ToDbTime();
                }))
                .ForMember(dst => dst.UtcModified, opt => opt.ResolveUsing(db =>
                {
                    return db.Modified.ToDbTime();
                }))
                .ForMember(dst => dst.UtcDisabled, opt => opt.ResolveUsing(db =>
                {
                    return db.Disabled.ToDbTime();
                }))
                .ForMember(dst => dst.CreatedByUserPId, opt => opt.ResolveUsing(model =>
                {
                    if (model.CreatedBy == null || !model.CreatedBy.PId.HasValue)
                        return Guid.Empty;
                    return model.CreatedBy.PId.Value;
                }))
                .ForMember(dst => dst.ModifiedByUserPId, opt => opt.ResolveUsing(model =>
                {
                    if (model.ModifiedBy == null || !model.ModifiedBy.PId.HasValue)
                        return Guid.Empty;
                    return model.ModifiedBy.PId.Value;
                }))
                .ForMember(dst => dst.DisabledByUserPId, opt => opt.ResolveUsing(model =>
                {
                    if (model.DisabledBy == null) return null;
                    return model.DisabledBy.PId;
                }))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.InvoiceId, opt => opt.ResolveUsing(model =>
                {
                    if (model.Invoice == null || !model.Invoice.Id.HasValue)
                        return Guid.Empty;
                    return model.Invoice.Id.Value;
                }))
                .ForMember(dst => dst.FeeId, opt => opt.ResolveUsing(model =>
                {
                    if (model.Fee == null || !model.Fee.Id.HasValue)
                        return Guid.Empty;
                    return model.Fee.Id.Value;
                }))
                .ForMember(dst => dst.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dst => dst.TaxAmount, opt => opt.MapFrom(src => src.TaxAmount))
                .ForMember(dst => dst.Details, opt => opt.MapFrom(src => src.Details));
        }
    }
}
